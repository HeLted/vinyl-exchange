import React, { Component } from 'react';
import UserSalesComponent from "./UserSalesComponent"
import { Url, Controllers } from "./../../../../constants/UrlConstants";
import axios from "axios";
import { NotificationContext } from "./../../../../contexts/NotificationContext";

class UserSalesContainer extends Component {
    constructor(props) {
        super(props);
        this.state = {
            sales:[],
            isLoading:false
          }
    }

    static contextType = NotificationContext;
    
  componentDidMount() {
    this.setState({ isLoading: true });
    axios
      .get(
        Url.api +
          Controllers.sales.name +
          Controllers.sales.actions.getUserSales
      )
      .then(response => {
        this.setState({ sales: response.data, isLoading: false });
        this.context.handleAppNotification("Loaded user sales!", 5);
      })
      .catch(error => {
        this.setState({ isLoading: false });
        this.context.handleServerNotification(
          error.response,
          "Failed to load user sales!"
        );
      });
  }

    render() { 
        return (<UserSalesComponent data={{sales:this.state.sales,isLoading:this.state.isLoading}}/>);
    }
}
 
export default UserSalesContainer;