import React from "react";

function MoneyInput(props) {
  const extraClasses =
    props.extraClasses === undefined ? "" : " " + props.extraClasses;

  return (
    <input
      type="number"
      className={"form-control" + extraClasses}
      id={props.id}
      value={props.value}
      name={props.id}
      step="any"
      onChange={props.onChange}
      placeholder={props.placeholder}
    />
  );
}

export default MoneyInput;
