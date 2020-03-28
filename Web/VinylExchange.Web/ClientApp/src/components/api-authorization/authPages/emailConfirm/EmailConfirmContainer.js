import React, { Component } from "react";
import EmailConfirmComponent from "./EmailConfirmComponent";
import {
  Url,
  Controllers,
  Queries
} from "../../../../constants/UrlConstants";
import axios from "axios";
import { NotificationContext } from "../../../../contexts/NotificationContext";
import  getAntiForgeryAxiosConfig from "../../../../functions/getAntiForgeryAxiosConfig";

class EmailConfirmContainer extends Component {
  constructor() {
    super();
    this.state = {
      emailConfirmToken: "",
      isLoading: false
    };
  }

  static contextType = NotificationContext;

  componentDidMount() {
    let confirmToken = this.props.location.search.replace(
      Url.queryStart + Queries.cofirmToken + Url.equal,
      ""
    );

    if (confirmToken !== "") {
      this.setState({ emailConfirmToken:confirmToken });
    } else {
      this.props.history.push("/");
    }
  }

  handleOnClick = () => {
    this.setState({ isLoading: true });

    const submitFormObj = {
      emailConfirmToken: this.state.emailConfirmToken,
    };
    axios
      .post(
        Url.api +
          Controllers.users.name +
          Controllers.users.actions.confirmEmail,
        submitFormObj,
        getAntiForgeryAxiosConfig()
      )
      .then(response => {
        this.setState({ isLoading: false });
        this.context.handleAppNotification("Email Confirmed", 4);
        this.props.history.push("/Authentication/Logout");
      })
      .catch(error => {
        this.setState({ isLoading: false });
        this.context.handleServerNotification(
          error.response,
          "There was an error in confirming your email!"
        );
      });
  };

  render() {
    return (
      <EmailConfirmComponent
        data={{ isLoading: this.state.isLoading }}
        functions={{ handleOnClick: this.handleOnClick }}
      />
    );
  }
}

export default EmailConfirmContainer;
