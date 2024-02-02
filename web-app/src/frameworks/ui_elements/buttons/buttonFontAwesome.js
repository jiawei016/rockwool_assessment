export const DefaultButtonFontAwesome = (props) => {
    return <button type="button" className={"btn btn-primary defaultButton " + props.class_name} onClick={props.button_clickEvent} value={props.button_text}>
        <i className={"fa " + props.icon_name}></i>
    </button>
}