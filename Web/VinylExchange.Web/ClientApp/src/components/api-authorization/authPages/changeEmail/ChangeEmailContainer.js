import React, { Component } from "react";
import ChangeEmailComponent from "./ChangeEmailComponent";
import { Url, Controllers, Queries } from "../../../../constants/UrlConstants";
import axios from "axios";
import { NotificationContext } from "../../../../contexts/NotificationContext";
import getAntiForgeryAxiosConfig from "../../../../functions/getAntiForgeryAxiosConfig";

class ChangeEmailContainer extends Component {
  constructor() {
    super();
    this.state = {
      newEmailInput: "",
      changeEmailToken :"",
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
      this.setState({ changeEmailToken: confirmToken });
    } else {
      this.props.history.push("/");
    }
  }

  handleOnChange = event => {
    const { value, name } = event.target;
    this.setState({ [name]: value });
  };

  handleOnSubmit = () => {
    this.setState({ isLoading: true });

    const submitFormObj = {
      changeEmailToken: this.state.changeEmailToken,
      newEmail:this.state.newEmailInput
    };
    axios
      .post(
        Url.api +
          Controllers.users.name +
          Controllers.users.actions.changeEmail,
        submitFormObj,
        getAntiForgeryAxiosConfig()
      )
      .then(response => {
        this.setState({ isLoading: false });
        this.context.handleAppNotification("Email succesfully changed", 4);
        this.props.history.push("/Authentication/Logout");
      })
      .catch(error => {
        this.setState({ isLoading: false });
        this.context.handleServerNotification(
          error.response,
          "There was an error in changing your email!"
        );
      });
  };

  render() {
    return (
      <ChangeEmailComponent
        data={{
          newEmailInput: this.state.newEmailInput,
          isLoading: this.state.isLoading
        }}
        functions={{
          handleOnChange: this.handleOnChange,
          handleOnSubmit: this.handleOnSubmit
        }}
      />
    );
  }
}

export default ChangeEmailContainer;
