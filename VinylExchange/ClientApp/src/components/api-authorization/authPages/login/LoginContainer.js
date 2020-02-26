import React, { Component } from 'react';
import LoginComponent from "./LoginComponent"
import {
  Url,
  Controllers,
  Queries
} from "./../../../../constants/UrlConstants";
import axios from "axios";
import {NotificationContext} from "./../../../../contexts/NotificationContext"
import authService from "./../../../api-authorization/AuthorizeService"
import urlPathSeparator from "./../../../../functions/urlPathSeparator"

class LoginContainer extends Component {
    constructor() {
        super();
        this.state = {
          usernameInput: "",
          passwordInput: "",
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
          password: this.state.passwordInput,
        };
    
        
        axios
          .post(
            Url.authentication +
              Controllers.users.name +
              Controllers.users.actions.login,
            submitFormObj
          )
          .then(async(response) => {
            if(response.status === 200){

                console.log("hiii");
                let redirectUrl = this.props.location.search.replace(Url.queryStart + Queries.returnUrl + Url.equal,"");
                redirectUrl = urlPathSeparator(redirectUrl);
                const state = { returnUrl:redirectUrl };
                await authService.signIn(state);
                this.context.handleAppNotification("Succesfully logged in", 4);
                if(redirectUrl === ""){
                    redirectUrl = "/"
                      this.props.history.push(redirectUrl)
                }else{
                 
                    this.props.history.push(redirectUrl)
                }
            }
            
          })
          .catch(error => {
            this.context.handleServerNotification(
              error.response,
              "There was an error in logging you in!"
            );
          });
      };
    
      render() {
        return (
          <LoginComponent
            data={{
              usernameInput: this.state.usernameInput,
              passwordInput: this.state.passwordInput,
            }}
            functions={{
              handleOnChange: this.handleOnChange,
              handleOnSubmit: this.handleOnSubmit
            }}
          />
        );
      }
    }
 
export default LoginContainer;