import '../index.css'

export const DefaultInputText = (props) => {
    return <input id={props.input_name} name={props.input_name} type={props.input_type} className={'form-control defaultInput ' + props.class_name} ref={props.reference}/>;
  };
