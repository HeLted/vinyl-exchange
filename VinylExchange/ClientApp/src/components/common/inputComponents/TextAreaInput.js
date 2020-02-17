import React from "react";

function TextAreaInput(props) {
  return (
    <textarea
      className="form-control"
      id={props.id}
      name={props.id}
      rows={props.rows}
      onChange={props.onChange}
    ></textarea>
  );
}

export default TextAreaInput;
