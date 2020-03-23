import React, { Component } from "react";
import ReleaseSalesComponent from "./ReleaseSalesComponent";
import axios from "axios";
import {
  Url,
  Controllers,
  Queries
} from "./../../../../constants/UrlConstants";
import { NotificationContext } from "./../../../../contexts/NotificationContext";

class ReleaseSalesContainer extends Component {
  constructor() {
    super();
    this.state = {
      sales: [],
      isLoading: false
    };
  }

  static contextType = NotificationContext;

  componentDidMount() {
    this.setState({ isLoading: true });
    axios
      .get(
        Url.api +
          Controllers.sales.name +
          Controllers.sales.actions.getAllSalesForRelease +
          Url.slash +
          this.props.data.releaseId
      )
      .then(response => {
        this.context.handleAppNotification("Loaded release sales", 5);
        this.setState({ sales: response.data, isLoading: false });
      })
      .catch(error => {
        this.context.handleServerNotification(
          error.response,
          "Failed to load release sales"
        );
      });
  }

  render() {
    return (
      <ReleaseSalesComponent
        data={{ sales: this.state.sales, isLoading: this.state.isLoading }}
      />
    );
  }
}

export default ReleaseSalesContainer;
