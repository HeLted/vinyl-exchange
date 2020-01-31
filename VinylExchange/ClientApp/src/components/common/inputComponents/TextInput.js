import React from "react";

export default function TextInput(props) {
  const extraClasses =
    props.extraClasses === undefined ? "" : " " + props.extraClasses;

  return (
    <input
      type="text"
      className={"form-control" + extraClasses}
      id={props.id}
      value={props.value}
      onChange={props.onChange}
      placeholder={props.placeholder}
    />
  );
}
