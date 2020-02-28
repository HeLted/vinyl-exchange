import React, { Component } from "react";
import AddShopComponent from "./AddShopComponent";
import uuidv4 from "../../../functions/guidGenerator";
import { Url, Controllers, Queries } from "../../../constants/UrlConstants";
import axios from "axios";
import { NotificationContext } from "./../../../contexts/NotificationContext";
import  getAntiForgeryAxiosConfig from "./../../../functions/getAntiForgeryAxiosConfig"

class AddShopContainer extends Component {
  constructor() {
    super();
    this.state = {
      formSessionId: uuidv4(),
      shopNameInput: "",
      shopTypeSelectInput: "",
      webAddressInput:"",
      countryInput:"",
      townInput:"",
      addressInput:""
    
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
      name: this.state.shopNameInput,
      shopType: this.state.shopTypeSelectInput,
      webAddress: this.state.webAddressInput,
      country: this.state.coutry,
      town: this.state.townInput,
      address: this.state.addressInput
    };

    const self = this;
    axios
      .post(
        Url.api +
          Controllers.shops.name +
          Url.slash +
          Url.queryStart +
          Queries.formSessionId +
          Url.equal +
          this.state.formSessionId,
        submitFormObj,
        getAntiForgeryAxiosConfig()
      )
      .then(response => {
        self.context.handleAppNotification("Succesfully created shop", 4);
      })
      .catch(error => {
        self.context.handleServerNotification(
          error.response,
          "There was An error when creating shop!"
        );
      });
  };

  componentWillUnmount() {
    fetch(
      Url.api +
        Controllers.files.name +
        Controllers.files.actions.deleteAll +
        Url.queryStart +
        Queries.formSessionId +
        Url.equal +
        this.state.formSessionId,
      {
        method: "DELETE",

        headers: {
          "Content-Type": "application/json"
        }
      }
    )
      .then(function(response) {
        console.log(response);
      })
      .catch(error => {
        this.context.handleServerNotification(
          error.response,
          "There was an error on the server!"
        );
      });
  }

  render() {
    return (
      <AddShopComponent
        functions={{
          handleOnChange: this.handleOnChange,
          handleOnChangeMultiSelect: this.handleOnChangeMultiSelect,
          handleOnSubmit: this.handleOnSubmit
        }}
        state={this.state}
      />
    );
  }
}

export default AddShopContainer;
