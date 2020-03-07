import React from "react";
import Label from "./../../../common/inputComponents/Label";
import TextInput from "./../../../common/inputComponents/TextInput";
import PasswordInput from "./../../../common/inputComponents/PasswordInput";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import { faSync } from "@fortawesome/free-solid-svg-icons";
import "./../authForm.css";

function LoginComponent(props) {
  return (
    <div className="container-fluid">
      <div className="row justify-content-center">
        <div className="auth-form col-4 border">
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

            <div class="form-group">
              <div class="form-check text-center">
                <div class="custom-control custom-checkbox">
                  <input
                    type="checkbox"
                    className="custom-control-input"
                    id="rememberMeInput"
                    onChange={props.functions.handleOnChange}
                    checked={props.data.rememberMeInput}
                  />
                  <label
                    className="custom-control-label"
                    htmlFor="rememberMeInput"
                  >
                    Remember Me ?
                  </label>
                </div>
              </div>
            </div>

            <div className="text-center">
              {props.data.isLoading ? (
                <button class="btn btn-primary" disabled>
                  <FontAwesomeIcon icon={faSync} spin />
                </button>
              ) : (
                <button type="submit" class="btn btn-primary">
                  Submit
                </button>
              )}
            </div>
          </form>
        </div>
      </div>
    </div>
  );
}

export default LoginComponent;
