import React from "react";
import Label from "./../../../common/inputComponents/Label";
import TextInput from "./../../../common/inputComponents/TextInput";

function ChangeEmailComponent(props) {
  return (
    <div className="auth-form container-fluid border">
      <div className=" row justify-content-center ">
        <div className="col-5">
          <div className="form-group">
            <Label
              for="newEmailInput"
              value="Enter your new email again (for security purpouses)"
            />
            <TextInput
              specialType="email"
              id="newEmailInput"
              placeholder="New Email..."
              value={props.data.newEmailInput}
              onChange={props.functions.handleOnChange}
              required
              validateEmail
              validateLength
              minLength={0}
              maxLength={100}
            />
          </div>
        </div>
      </div>
      <div className="row justify-content-center ">
        <div className="form-group">
          <button
            className="btn btn-success btn-lg"
            onClick={props.functions.handleOnSubmit}
          >
            Change Email
          </button>
        </div>
      </div>
    </div>
  );
}

export default ChangeEmailComponent;
