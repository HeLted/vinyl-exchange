import React, { Component, ReactChild } from "react";
import { Container } from "reactstrap";
import { NavMenu } from "./NavMenu";
import Player from "./common/Player";
import { Helmet } from "react-helmet";
import ServerNotification from "./ServerNotification";
import NotificationContextProvider from "./../contexts/NotificationContext";

export class Layout extends Component {
  static displayName = Layout.name;

  render() {
    return (
      <div>
        <NavMenu />
        <NotificationContextProvider>
          <ServerNotification />
          <Container fluid={true}>
            {this.props.children}
            <Player />
          </Container>
        </NotificationContextProvider>
      </div>
    );
  }
}
