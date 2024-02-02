import React, { useRef, useState } from "react";
import {
  DefaultInputText,
  DefaultLabel,
  DefaultButtonSubmit,
} from "../../../frameworks/ui_elements/index";
import { RefValidation } from "../../../frameworks/validate/refValidation";
import styles from "../index.module.css";

import { reduxAdd } from "../../../stores/news/newsSlice";

import { ApiNewsService } from "../../../services/apiNewsService";
import { useDispatch, useSelector } from "react-redux";
import { PaginationComponent } from "../../../pages_components/pagination";

export const SearchNewsPage = () => {
  const ref_Search = useRef();
  const dispatch = useDispatch();
  const _newsState = useSelector((state) => state.newsState);
  const [param, setParam] = useState([]);

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
    ApiNewsService(param).subscribe((data) => {
      dispatch(reduxAdd(data, data.totalResults));
    });
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

      {_newsState.newsData.results.map((obj, index) => (
        <div className="card mt-4 mb-4" key={"newsCard-" + index}>
          <div className={styles.ribbon}>{obj.category[0]}</div>
          <div className="card-body">
            <h5 className={"card-title mt-3 " + styles.cardTitle}>
              {obj.title}
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

      <PaginationComponent
        paginationClickEvent={(e) => onPaginationClickEvent(e)}
        records={_newsState.newsData.totalResults}
      />
    </>
  );
};
