import React from "react";
import Label from "./../../../common/inputComponents/Label";
import TextInput from "./../../../common/inputComponents/TextInput";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import { faSync } from "@fortawesome/free-solid-svg-icons";
import "./../authForm.css";

function RegisterComponent(props) {
  return (
    <div className="container-fluid">
      <div className="row justify-content-center border">
        <h3 className="property-text">Register</h3>
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
                alphaNumericAndUnderscore
                required
                validateLength
                minLength={3}
                maxLength={50}
              />
            </div>
            <div className="form-group">
              <Label for="emailInput" value="E-Mail" />
              <TextInput
                specialType="email"
                id="emailInput"
                placeholder="E-Mail..."
                value={props.data.emailInput}
                onChange={props.functions.handleOnChange}
                required
                validateEmail
                validateLength
                minLength={0}
                maxLength={100}
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
                required
                validateLength
                minLength={8}
                maxLength={100}
              />
            </div>
            <div className="form-group">
              <Label for="confirmPasswordInput" value="Confirm Password" />
              <TextInput
                specialType="password"
                id="confirmPasswordInput"
                placeholder="Confirm Password..."
                value={props.data.confirmPasswordInput}
                onChange={props.functions.handleOnChange}
              />
            </div>
            <div className="text-center">
              {props.data.isLoading ? (
                <button className="btn btn-primary" disabled>
                  <FontAwesomeIcon icon={faSync} spin />
                </button>
              ) : (
                <button type="submit" className="btn btn-primary">
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
