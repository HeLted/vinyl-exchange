import React, { Component } from "react";
import ForgotPasswordComponent from "./ForgotPasswordComponent";
import { Url, Controllers, Queries } from "../../../../constants/UrlConstants";
import axios from "axios";
import { NotificationContext } from "../../../../contexts/NotificationContext";
import getAntiForgeryAxiosConfig from "../../../../functions/getAntiForgeryAxiosConfig";

class ForgotPasswordContainer extends Component {
  constructor(props) {
    super(props);
    this.state = {
      emailInput: "",
      resetPasswordTokenInput: "",
      newPasswordInput: "",
      confirmPasswordInput: "",
      isLoading: false,
      formPhase: 1
    };
  }

  static contextType = NotificationContext;

  handleOnChange = event => {
    const { value, name } = event.target;
    this.setState({ [name]: value });
  };

  handleOnSubmitEmail = () => {
    const submitObj = {
      email: this.state.emailInput
    };

    axios
      .post(
        Url.api +
          Controllers.users.name +
          Controllers.users.actions.sendResetPasswordEmail,
        submitObj,
        getAntiForgeryAxiosConfig()
      )
      .then(response => {
        this.context.handleAppNotification(
          "Password reset token has been sent to your e-mail",
          4
        );
        this.setState({ formPhase: 2 });
      })
      .catch(error => {
        const errorMessage =
          error.response.data != ""
            ? error.response.data
            : "Failed to send password reset email!";

        this.context.handleServerNotification(error.response, errorMessage);
      });
  };

  handleOnSubmit = () => {
    const submitObj = {
      email: this.state.emailInput,
      resetPasswordToken: this.state.resetPasswordTokenInput,
      newPassword: this.state.newPasswordInput,
      confirmPassword: this.state.confirmPasswordInput
    };

    axios
      .post(
        Url.api +
          Controllers.users.name +
          Controllers.users.actions.resetPassword,
        submitObj,
        getAntiForgeryAxiosConfig()
      )
      .then(response => {
        this.context.handleAppNotification(
          "Sucessfully setted new password on your account",
          4
        );
        this.props.history.push("/Authentication/Login");
      })
      .catch(error => {
        this.context.handleServerNotification(error.response,"Failed to reset password!");
      });
  };

  render() {
    return (
      <ForgotPasswordComponent
        data={{
          emailInput: this.state.emailInput,
          resetPasswordTokenInput: this.state.resetPasswordTokenInput,
          newPasswordInput: this.state.newPasswordInput,
          confirmPasswordInput: this.state.confirmPasswordInput,
          isLoading: this.state.isLoading,
          formPhase: this.state.formPhase
        }}
        functions={{
          handleOnChange: this.handleOnChange,
          handleOnSubmit: this.handleOnSubmit,
          handleOnSubmitEmail: this.handleOnSubmitEmail
        }}
      />
    );
  }
}

export default ForgotPasswordContainer;
