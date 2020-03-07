import React, { Component, ReactChild } from "react";
import { Container } from "reactstrap";
import { NavMenu } from "./NavMenu";
import Player from "./layoutComponents/Player/Player";
import { Helmet } from "react-helmet";
import ServerNotification from "./layoutComponents/ServerNotification/ServerNotification";
import NotificationContextProvider from "./../contexts/NotificationContext";
import PlayerContextProvider from "./../contexts/PlayerContext";
import Footer from "./Footer"

export class Layout extends Component {
  static displayName = Layout.name;

  render() {
    return (
      <div>
        <NavMenu />
        <NotificationContextProvider>
          <PlayerContextProvider>
            <ServerNotification />
            <Container fluid={true}>{this.props.children}</Container>
            <Player />
          </PlayerContextProvider>
        </NotificationContextProvider>
        <Footer/>
      </div>
    );
  }
}
