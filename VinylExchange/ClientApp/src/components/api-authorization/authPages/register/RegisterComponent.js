import React from "react";
import Label from "./../../../common/inputComponents/Label";
import TextInput from "./../../../common/inputComponents/TextInput";
import EmailInput from "./../../../common/inputComponents/EmailInput";
import PasswordInput from "./../../../common/inputComponents/PasswordInput";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import {  faSync } from "@fortawesome/free-solid-svg-icons";
import "./../authForm.css"

function RegisterComponent(props) {
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
            <Label for="emailInput" value="E-Mail" />
              <EmailInput
                id="emailInput"
                placeholder="E-Mail..."
                value={props.data.emailInput}
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
            <Label for="confirmPasswordInput" value="Confirm Password" />
              <PasswordInput
                id="confirmPasswordInput"
                placeholder="Confirm Password..."
                value={props.data.confirmPasswordInput}
                onChange={props.functions.handleOnChange}
              />
            </div>
            <div className="text-center">
              {props.data.isLoading ? (
                <button class="btn btn-primary"> 
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

export default RegisterComponent;
