import React, { Component } from 'react';
import { Container } from 'reactstrap';
import { NavMenu } from './NavMenu';
import Player from './common/Player'

export class Layout extends Component {
  static displayName = Layout.name;

  render () {
    return (
      <div>
        <NavMenu />
        <Container fluid={true}>
          {this.props.children}
        </Container>
        <Player/>
      </div>
    );
  }
}
