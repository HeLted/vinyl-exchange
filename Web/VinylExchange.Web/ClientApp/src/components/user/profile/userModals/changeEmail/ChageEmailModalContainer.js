import React, { Component } from "react";
import ChangeEmailModalComponent from "./ChangeEmailModalComponent";
import axios from "axios";
import getAntiForgeryAxiosConfig from "./../../../../../functions/getAntiForgeryAxiosConfig";
import {
  Url,
  Controller,
  Controllers
} from "./../../../../../constants/UrlConstants";
import { NotificationContext } from "./../../../../../contexts/NotificationContext";

class ChangeEmailModalContainer extends Component {
  constructor(props) {
    super(props);
    this.state = {
      newEmailInput: "",
      isLoading: false,
      isEmailSend:false
    };
  }

  static contextType = NotificationContext;

  handleOnChange = event => {
    const { value, name } = event.target;
    this.setState({ [name]: value });
  };
  handleOnSubmit = () => {
    const submitObj = {
      newEmail: this.state.newEmailInput
    };

    axios
      .post(
        Url.api +
          Controllers.users.name +
          Controllers.users.actions.sendChangeEmailEmail,
        submitObj,
        getAntiForgeryAxiosConfig()
      )
      .then(response => {
        this.context.handleAppNotification(
          "Change email confirmation was send to your old email",
          4
        );
        this.setState({ isEmailSend: true, isLoading: false });
      })
      .catch(error => {
        this.context.handleServerNotification(
          error.response,
          "Failed to send change email confirmation email!"
        );
      });
  };

  render() {
    return (
      <ChangeEmailModalComponent
        data={{
          newEmailInput: this.state.newEmailInput,
          isLoading: this.state.isLoading,
          isEmailSend:this.state.isEmailSend
        }}
        functions={{
          handleOnSubmit: this.handleOnSubmit,
          handleOnChange: this.handleOnChange
        }}
      />
    );
  }
}

export default ChangeEmailModalContainer;
