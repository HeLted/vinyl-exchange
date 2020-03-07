import React from "react";

function CheckBoxInput(props) {
  return (
    <input
      type="checkbox"
      class="form-check-input"
      id={props.id}
      name={props.id}
      value={props.value}
    />
  );
}

export default CheckBoxInput;
