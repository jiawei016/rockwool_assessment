import React, { useRef } from "react";
import {DefaultInputText, DefaultLabel, DefaultButtonSubmit} from '../../../frameworks/ui_elements/index';
import {RefValidation} from '../../../frameworks/validate/refValidation';

import styles from '../index.module.css';

export const SearchNewsPage = () => {
  const ref_Search = useRef();

  const onSearchEvent =() => {
    console.log('onSearchEvent');
    if(RefValidation(ref_Search) === true){

    }
    else{
        ref_Search.current.focus();
    }
    
  }

  return (
    <>
        <div className={"mb-3 " + styles.Card}>
            <DefaultLabel html_for={"searchTitle"} label_text={"Title"} />
            <div className="p-2"></div>
            <DefaultInputText input_name={"searchTitle"} input_type={"text"} reference={ref_Search} />
            <DefaultButtonSubmit button_text={"Search"} button_clickEvent={() => onSearchEvent()}/>
        </div>
        

    </>
  );
};
