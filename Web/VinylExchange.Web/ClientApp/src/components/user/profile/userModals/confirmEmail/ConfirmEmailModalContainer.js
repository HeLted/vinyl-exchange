import React, { Component } from "react";
import ConfirmEmailModalComponent from "./ConfirmEmailModalComponent";
import axios from "axios";
import getAntiForgeryAxiosConfig from "./../../../../../functions/getAntiForgeryAxiosConfig";
import { Url, Controllers } from "./../../../../../constants/UrlConstants";
import { NotificationContext } from "./../../../../../contexts/NotificationContext";

class ConfirmEmailModalContainer extends Component {
  constructor(props) {
    super(props);
    this.state = {
      isEmailSend: false,
      isLoading: false
    };
  }

  static contextType = NotificationContext;

  handleOnSubmit = () => {
    this.setState({ isLoading: true });
    axios
      .post(
        Url.api +
          Controllers.users.name +
          Controllers.users.actions.sendConfirmEmail,
          {},
        getAntiForgeryAxiosConfig()
      )
      .then(response => {
        this.context.handleAppNotification(
          "Confirmation Email send to your email",
          4
        );
        this.setState({ isEmailSend: true, isLoading: false });
      })
      .catch(error => {
        this.context.handleServerNotification(
          error.response,
          "Failed to send confirmation email!"
        );
      });
  };

  render() {
    return (
      <ConfirmEmailModalComponent
        data={{
          isEmailSend: this.state.isEmailSend,
          isLoading: this.state.isLoading
        }}
        functions={{handleOnSubmit:this.handleOnSubmit}}
      />
    );
  }
}

export default ConfirmEmailModalContainer;
