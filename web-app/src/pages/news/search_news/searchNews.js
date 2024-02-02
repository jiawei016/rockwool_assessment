import React, { useRef, useState } from "react";
import {
  DefaultInputText,
  DefaultLabel,
  DefaultButtonSubmit,
} from "../../../frameworks/ui_elements/index";
import { NewsDetail } from "./Modals/index";
import { RefValidation } from "../../../frameworks/validate/refValidation";
import styles from "../index.module.css";

import { reduxAdd } from "../../../stores/news/newsSlice";

import {
  ApiNewsService,
  ApiNewsSearchByPaginationService,
} from "../../../services/apiNewsService";
import { useDispatch, useSelector } from "react-redux";
import { PaginationComponent } from "../../../pages_components/pagination";

export const SearchNewsPage = () => {
  const ref_Search = useRef();
  const dispatch = useDispatch();
  const _newsState = useSelector((state) => state.newsState);
  const [param, setParam] = useState([]);
  const [newsDetailShow, setNewsDetailShow] = useState(false);
  const [newsDetail, setNewsDetail] = useState(null);

  const onSearchEvent = () => {
    let param = [];
    if (RefValidation(ref_Search) === true) {
      param.push(ref_Search.current.value);
      setParam(param);
      ApiNewsService(param).subscribe((data) => {
        dispatch(reduxAdd(data, data.totalResults));
      });
    } else {
      ref_Search.current.focus();
    }
  };

  const onPaginationClickEvent = (pageNumber) => {
    let _paramJson = JSON.stringify(param);
    let _paramObj = JSON.parse(_paramJson);
    _paramObj.push(pageNumber);
    ApiNewsSearchByPaginationService(_paramObj).subscribe((data) => {
      dispatch(reduxAdd(data, data.totalResults));
    });
  };

  const onNewsDetailClickHandler = (newsObj) => {
    setNewsDetail(newsObj);
    setNewsDetailShow(true);
  };

  return (
    <>
      <div className={"mb-3 " + styles.Card}>
        <DefaultLabel html_for={"searchTitle"} label_text={"Title"} />
        <div className="p-2"></div>
        <DefaultInputText
          input_name={"searchTitle"}
          input_type={"text"}
          reference={ref_Search}
        />
        <DefaultButtonSubmit
          button_text={"Search"}
          button_clickEvent={() => onSearchEvent()}
        />
      </div>

      <PaginationComponent
        paginationClickEvent={(e) => onPaginationClickEvent(e)}
        records={_newsState.newsData.totalResults}
      />

      {_newsState.newsData.results.map((obj, index) => (
        <div
          className="card mt-4 mb-4"
          key={"newsCard-" + index}
          onClick={() => onNewsDetailClickHandler(obj)}
        >
          <div className={styles.ribbon}>{obj.category[0]}</div>
          <div className="card-body">
            <h5 className={"card-title mt-3 " + styles.cardTitle}>
              {obj.title.substring(0, 30) + "..."}
            </h5>
          </div>
          <div className="card-body">
            {obj.country.map((obj1, index) => (
              <span className="badge bg-dark" key={"country_" + index}>
                {obj1}
              </span>
            ))}
          </div>
        </div>
      ))}

      <NewsDetail
        show={newsDetailShow}
        handle_close={() => setNewsDetailShow(false)}
        detail={newsDetail}
      />
    </>
  );
};
