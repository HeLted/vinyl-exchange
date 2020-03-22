import React, { Component } from "react";
import UserPurchasesComponent from "./UserPurchasesComponent";
import axios from "axios";
import { NotificationContext } from "./../../../../contexts/NotificationContext";
import { Url, Controllers } from "./../../../../constants/UrlConstants";
import { withRouter } from 'react-router-dom';

class UserPurchasesContainer extends Component {
  constructor(props) {
    super(props);
    this.state = {
      purchases: [],
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
          Controllers.sales.actions.getUserPurchases
      )
      .then(response => {
        this.setState({ purchases: response.data, isLoading: false });
        this.context.handleAppNotification("Load user purchases!", 5);
      })
      .catch(error => {
        this.setState({ isLoading: false });
        this.context.handleServerNotification(
          error.response,
          "Failed to load user purchases!"
        );
      });
  }

  handleGoToSale = saleId => {
   this.props.history.push(`/Sales/${saleId}`);
  };

  render() {
    return (
      <UserPurchasesComponent
        data={{
          purchases: this.state.purchases,
          isLoading: this.state.isLoading
        }}
        functions={{ handleGoToSale: this.handleGoToSale }}
      />
    );
  }
}

function UserPurchasesContainerWrapper(props) {
  return <UserPurchasesContainer {...props} />;
}

export default withRouter(UserPurchasesContainerWrapper);
