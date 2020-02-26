import React from "react";

function PasswordInput(props) {
  const extraClasses =
    props.extraClasses === undefined ? "" : " " + props.extraClasses;

  return (
    <input
      type="password"
      className={"form-control" + extraClasses}
      id={props.id}
      value={props.value}
      name={props.id}
      onChange={props.onChange}
      placeholder={props.placeholder}
    />
  );
}

export default PasswordInput;