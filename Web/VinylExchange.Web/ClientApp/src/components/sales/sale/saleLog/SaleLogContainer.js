import React, { Component } from 'react';
import SaleLogComponent from "./SaleLogComponent"
import * as SignalR from "@microsoft/signalr";
import { NotificationContext } from "./../../../../contexts/NotificationContext";
import { Url, Controllers } from "./../../../../constants/UrlConstants";
import axios from "axios";

class SaleLogContainer extends Component {
    constructor(props) {
        super(props);
        this.state = { 
            logs:[]
         }
    }

    static contextType = NotificationContext;

    componentDidMount() {
      this.setState({ isLoading: true });
  
      this.connection = new SignalR.HubConnectionBuilder()
        .withUrl("/Sale/LogHub")
        .build();
  
      this.connection.on("RecieveNewLog", this.recieveMessage);
  
      this.connection
        .start()
        .then(() => {
          this.context.handleAppNotification(
            "Established connection with Log Hub",
            5
          );
        
        })
        .catch(error => {
          
            
        });
    }

    render() { 
        return (<SaleLogComponent/>);
    }
}
 
export default SaleLogContainer;