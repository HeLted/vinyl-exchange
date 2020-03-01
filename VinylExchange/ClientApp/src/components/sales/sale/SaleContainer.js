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
      sale : {
      status: 0,
      sellerId: "",
      buyerId: "",
      sellerId: "",
      releaseId: "",
      buyerUsername: "",
      sellerUsername: "",
      vinylCondition: 0,
      sleeveCondition: 0,
      price:0,
      description:"",
      statusText:"",
      vinylConditionText:"",
      sleeveConditionText:"",
      }

    };
  }

  componentDidMount() {

    axios
      .get(
        Url.api +
          Controllers.sales.name +
          Url.slash +
          this.props.location.pathname.replace("/sale/","")
      )
      .then(response => {
        const data = response.data;
        this.context.handleAppNotification("Loaded sale data", 5);
        this.setState({sale:{
          status: data.status,
          sellerId: data.sellerId,
          buyerId: data.buyerId,
          sellerUsername: data.sellerUsername,
          buyerUsername: data.buyerUsername,
          releaseId:data.releaseId,
          vinylCondition:data.vinylCondition,
          sleeveCondition:data.sleeveCondition,
          price:data.price,
          description: data.description,
          statusText:data.statusText,
          vinylConditionText:data.vinylConditionText,
          sleeveConditionText:data.sleeveConditionText
        }});
      })
      .catch(error => {
        this.context.handleServerNotification(error.response);
      });
  }

  render() {
    return <SaleComponent data={{ sale: this.state.sale }} />;
  }
}

export default SaleContainer;
