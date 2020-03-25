import React from "react";

function InputValidationMessage(props) {
  return (
    <small  className="form-text text-danger">
      -{props.message}
    </small>
  );
}

export default InputValidationMessage;
