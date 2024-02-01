export const DefaultButtonSubmit = (props) => {
    return <input type="button" className={"btn btn-primary defaultButton " + props.class_name} onClick={props.button_clickEvent} value={props.button_text}/>
}