import React, { Component } from "react";
import { Container } from "reactstrap";
import { NavMenu } from "./NavMenu";
import Player from "./common/Player";
import { Helmet } from "react-helmet";
import ServerNotification from "./ServerNotification"

export class Layout extends Component {
  static displayName = Layout.name;

  constructor() {
    super();
    this.state = {
      currentNotificationMessage: "",
      currentNotificationSeverity: 0
    };
   
  }

  handleNotification = (notificationMessageAndSeverty) => {
   console.log("in")
    //format: ErrorHappened<->2
    let splitted = notificationMessageAndSeverty.split("<->")
    this.setState({
      
      currentNotificationMessage: splitted[0],
      currentNotificationSeverity: parseInt(splitted[0])
    });
  };

 

  render() {
    

    return (
      <div>
        <Helmet>
          <script src="/js/dropzone.js"></script>
        </Helmet>
        <NavMenu />
        <ServerNotification message={this.state.currentNotificationMessage} severity={this.state.severity}/>
        <Container globalstate={this} fluid={true}>
          {this.props.children}
          <Player />
        </Container>
      </div>
    );
  }
}
