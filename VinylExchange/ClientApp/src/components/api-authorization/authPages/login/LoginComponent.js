import React from "react";
import Label from "./../../../common/inputComponents/Label";
import TextInput from "./../../../common/inputComponents/TextInput";
import PasswordInput from "./../../../common/inputComponents/PasswordInput";

function LoginComponent(props) {
  return (
    <div className="container-fluid">
      <div className="row justify-content-center">
        <div className="col-6">
          <form onSubmit={props.functions.handleOnSubmit}>
            <div class="form-group">
              <Label for="usernameInput" value="Username" />
              <TextInput
                id="usernameInput"
                placeholder="Username..."
                value={props.data.usernameInput}
                onChange={props.functions.handleOnChange}
              />
            </div>

            <div class="form-group">
              <Label for="passwordInput" value="Password" />
              <PasswordInput
                id="passwordInput"
                placeholder="Password..."
                value={props.data.passwordInput}
                onChange={props.functions.handleOnChange}
              />
            </div>

            <button type="submit" class="btn btn-primary">
              Submit
            </button>
          </form>
        </div>
      </div>
    </div>
  );
}

export default LoginComponent;
