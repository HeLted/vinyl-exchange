import React, { Component } from "react";
import RegisterComponent from "./RegisterComponent";
import {
  Url,
  Controllers,
  Queries
} from "./../../../../constants/UrlConstants";
import axios from "axios";
import {NotificationContext} from "./../../../../contexts/NotificationContext"


class RegisterContainer extends Component {
  constructor() {
    super();
    this.state = {
      usernameInput: "",
      emailInput: "",
      passwordInput: "",
      confirmPasswordInput: ""
    };
  }

  static contextType = NotificationContext;

  handleOnChange = event => {
    const { value, name } = event.target;
    this.setState({ [name]: value });
  };

  handleOnSubmit = event => {
    event.preventDefault();
    event.stopPropagation();

    const submitFormObj = {
      username: this.state.usernameInput,
      email: this.state.emailInput,
      password: this.state.passwordInput,
      confirmPassword: this.state.confirmPasswordInput
    };

    
    axios
      .post(
        Url.authentication +
          Controllers.users.name +
          Controllers.users.actions.register,
        submitFormObj
      )
      .then(response => {
        if(response.status === 200){
          this.context.handleAppNotification("Succesfully registered", 4);
          this.props.history.push("/Authentication/Login");
        }
        
      })
      .catch(error => {
        this.context.handleServerNotification(
          error.response,
          "There was an error in registering account!"
        );
      });
  };

  render() {
    return (
      <RegisterComponent
        data={{
          usernameInput: this.state.usernameInput,
          emailInput: this.state.emailInput,
          passwordInput: this.state.passwordInput,
          confirmPasswordInput: this.state.confirmPasswordInput
        }}
        functions={{
          handleOnChange: this.handleOnChange,
          handleOnSubmit: this.handleOnSubmit
        }}
      />
    );
  }
}


export default RegisterContainer

