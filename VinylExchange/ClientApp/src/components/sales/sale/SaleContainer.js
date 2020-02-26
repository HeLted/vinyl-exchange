import React, { Component } from "react";
import SaleComponent from "./SaleComponent";
import axios from "axios";
import { Url, Controllers, Queries } from "./../../../constants/UrlConstants";
import { NotificationContext } from "./../../../contexts/NotificationContext";
import authService from "./../../../components/api-authorization/AuthorizeService";

class SaleContainer extends Component {
  static contextType = NotificationContext;

  constructor() {
    super();
    this.state = {
      status: 0
    };
  }

  componentDidMount() {
    axios
      .get(
        Url.api +
          Controllers.sales.name +
          Url.slash +
          this.props.location.state.saleId
      )
      .then(response => {
        const data = response.data
        this.context.handleAppNotification("Loaded sale data", 5);
        this.setState({status:data.status})
      })
      .catch(error => {
        this.context.handleServerNotification(error.response);
      });

     
  }

  render() {
    return <SaleComponent data={{status:this.state.status}}/>;
  }
}

export default SaleContainer;
