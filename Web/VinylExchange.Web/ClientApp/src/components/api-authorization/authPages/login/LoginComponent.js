import React from "react";
import Label from "./../../../common/inputComponents/Label";
import TextInput from "./../../../common/inputComponents/TextInput";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import { faSync } from "@fortawesome/free-solid-svg-icons";
import "./../authForm.css";

function LoginComponent(props) {
  return (
    <div className="container-fluid">
      <div className="row justify-content-center border">
        <h3 className="property-text">Login</h3>
      </div>
      <br />
      <div className="row justify-content-center">
        <div className="auth-form col-4 border">
          <form onSubmit={props.functions.handleOnSubmit}>
            <div className="form-group">
              <Label for="usernameInput" value="Username" />
              <TextInput
                id="usernameInput"
                placeholder="Username..."
                value={props.data.usernameInput}
                onChange={props.functions.handleOnChange}
              />
            </div>

            <div className="form-group">
              <Label for="passwordInput" value="Password" />
              <TextInput
                specialType="password"
                id="passwordInput"
                placeholder="Password..."
                value={props.data.passwordInput}
                onChange={props.functions.handleOnChange}
              />
            </div>

            <div className="form-group">
              <div className="form-check text-center">
                <div className="custom-control custom-checkbox">
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
