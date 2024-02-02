export const DefaultButtonSubmit = (props) => {
    return <button type="button" className={"btn btn-primary defaultButton " + props.class_name} onClick={props.button_clickEvent}>{props.button_text}</button>
}