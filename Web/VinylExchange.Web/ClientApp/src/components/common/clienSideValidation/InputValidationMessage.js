import React from "react";

function InputValidationMessage(props) {
  return (
    <small id="passwordHelpBlock" className="form-text text-danger">
      -{props.message}
    </small>
  );
}

export default InputValidationMessage;
