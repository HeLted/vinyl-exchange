import React from "react";
import Label from "./../../../common/inputComponents/Label";
import TextInput from "./../../../common/inputComponents/TextInput";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import { faSync, faArrowRight } from "@fortawesome/free-solid-svg-icons";
import PageSpinner from "./../../../common/spinners/PageSpinner";

function ForgotPasswordComponent(props) {
  return props.data.isLoading ? (
    <PageSpinner />
  ) : (
    <div className="container-fluid" key={props.data.formPhase}>
      <div className="row justify-content-center border">
        <h3 className="property-text">Forgot Password</h3>
      </div>
      <br />
      <div className="row justify-content-center">
        {props.data.formPhase === 1 ? (
          <div className="auth-form col-4 border">
            <div className="form-group">
              <Label for="emailInput" value="Enter your e-mail" />
              <TextInput
                specialType="email"
                id="emailInput"
                placeholder="Enter your e-mail"
                value={props.data.emailInput}
                onChange={props.functions.handleOnChange}
                required
                validateEmail
                validateLength
                minLength={0}
                maxLength={100}
              />
            </div>

            <div className="text-center">
              {props.data.isLoading ? (
                <button className="btn btn-primary" disabled>
                  <FontAwesomeIcon icon={faSync} spin />
                </button>
              ) : (
                <button
                  className="btn btn-primary"
                  onClick={props.functions.handleOnSubmitEmail}
                >
                  Next <FontAwesomeIcon icon={faArrowRight} />
                </button>
              )}
            </div>
          </div>
        ) : (
          <div className="auth-form col-4 border">
            <div className="form-group">
              <Label
                for="resetPasswordTokenInput"
                value="Password Reset Token"
              />
              <TextInput
                id="resetPasswordTokenInput"
                placeholder="Enter the code that was send to your e-mail address.."
                value={props.data.resetPasswordTokenInput}
                onChange={props.functions.handleOnChange}
                required
              />
            </div>
            <div className="form-group">
              <Label for="newPasswordInput" value="New Password" />
              <TextInput
                specialType="password"
                id="newPasswordInput"
                placeholder="Password..."
                value={props.data.newPasswordInput}
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
                curerentPasswordValue={props.data.newPasswordInput}
                confirmPassword
              />
            </div>

            <div className="text-center">
              {props.data.isLoading ? (
                <button className="btn btn-primary" disabled>
                  <FontAwesomeIcon icon={faSync} spin />
                </button>
              ) : (
                <button
                  className="btn btn-primary"
                  onClick={props.functions.handleOnSubmit}
                >
                  Next <FontAwesomeIcon icon={faArrowRight} />
                </button>
              )}
            </div>
          </div>
        )}
      </div>
    </div>
  );
}

export default ForgotPasswordComponent;
