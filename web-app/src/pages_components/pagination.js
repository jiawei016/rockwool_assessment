import { useState } from "react";
import {
  DefaultButtonSubmit,
  DefaultButtonFontAwesome,
} from "../frameworks/ui_elements";
import styles from "./pagination.module.css";

export function PaginationComponent(props) {
  const [pageNumber, setPageNumber] = useState(1);

  let paging = props.records / 3;
  if (paging) {
    paging = Math.trunc(paging);
    paging = paging + 1;
  }

  let pagingArry = [];
  for (let i = 0; i < paging; i++) {
    pagingArry.push(i + 1);
  }

  const prevClick = () => {
    if (pageNumber > 1) {
      console.log(pageNumber - 1);
      props.paginationClickEvent(pageNumber - 1);
      setPageNumber(pageNumber - 1);
    }
  };

  const forwardClick = () => {
    if (pageNumber < paging) {
      console.log(pageNumber + 1);
      props.paginationClickEvent(pageNumber + 1);
      setPageNumber(pageNumber + 1);
    }
  };

  const pageNumberClick = (page) => {
    console.log(page);
    props.paginationClickEvent(page);
    setPageNumber(page);
  };

  return paging <= 0 ? (
    <></>
  ) : (
    <div className={styles.pagination}>
      <DefaultButtonFontAwesome
        icon_name={"bi-arrow-left-short"}
        class_name={styles.paginationPageNumber}
        button_clickEvent={() => prevClick()}
      />
      {pagingArry.map((page, index) => {
        return (
          <DefaultButtonSubmit
            button_text={page}
            class_name={styles.paginationPageNumber}
            button_clickEvent={() => pageNumberClick(page)}
            key={index}
          />
        );
      })}
      <DefaultButtonFontAwesome
        icon_name={"bi-arrow-right-short"}
        class_name={styles.paginationPageNumber}
        button_clickEvent={() => forwardClick()}
      />
    </div>
  );
}
