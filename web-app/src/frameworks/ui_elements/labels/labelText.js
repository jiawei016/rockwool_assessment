export const DefaultLabel = (props) => {
    return <label htmlFor={props.html_for} className={"form-label " + props.class_name}>{props.label_text}</label>;
  };
