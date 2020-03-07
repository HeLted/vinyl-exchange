import React from "react";

function TextInput(props) {
  const extraClasses =
    props.extraClasses === undefined ? "" : " " + props.extraClasses;

  return (
    <input
      type="text"
      className={"form-control" + extraClasses}
      id={props.id}
      value={props.value}
      name={props.id}
      onChange={props.onChange}
      placeholder={props.placeholder}
    />
  );
}

export default TextInput;