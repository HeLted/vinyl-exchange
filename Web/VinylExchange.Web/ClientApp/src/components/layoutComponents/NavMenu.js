import React, { Component } from "react";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import { faBars } from "@fortawesome/free-solid-svg-icons";
import {
  Collapse,
  Container,
  Navbar,
  NavbarBrand,
  NavbarToggler,
  NavItem,
  NavLink
} from "reactstrap";
import { Link } from "react-router-dom";
import "./NavMenu.css";
import  UserMenu  from "../api-authorization/UserMenu";

export class NavMenu extends Component {
  static displayName = NavMenu.name;

  constructor(props) {
    super(props);

    this.state = {
      collapsed: true
    };
  }

  shouldComponentUpdate(){
    if(window.innerWidth > 575){
      return false
    }else{
      return true
    }
  }

  handleToggleNavbar = () =>  {
    this.setState({
      collapsed: !this.state.collapsed
    });
  }

 

  render() {
    return (
      <header>
        <Navbar
          className="navbar-expand-sm navbar-toggleable-sm ng-white border-bottom box-shadow mb-3"
          light
        >
          <Container>
            <NavbarBrand tag={Link} to="/">
              <img src="img/appLogo.png" heigth="300px" width="150px" />
            </NavbarBrand>

            <div className="navbar-toggler" onClick={this.handleToggleNavbar}>
              <FontAwesomeIcon icon={faBars} color="white" />
            </div>
            <Collapse
              className="d-sm-inline-flex flex-sm-row-reverse"
              isOpen={!this.state.collapsed}
              navbar
            >
              <ul className="navbar-nav flex-grow">
                <li className="nav-item">
                  <NavLink
                    tag={Link}
                    className="navbar-link btn btn-outline-light text-light"
                    to="/"
                    onClick={this.handleToggleNavbar}
                  >
                    Home
                  </NavLink>
                </li>
                <li className="nav-item">
                  <NavLink
                    tag={Link}
                    className="navbar-link btn btn-outline-light text-light"
                    to="/Marketplace"
                    onClick={this.handleToggleNavbar}
                  >
                    Marketplace
                  </NavLink>
                </li>
                <UserMenu />
              </ul>
            </Collapse>
          </Container>
        </Navbar>
      </header>
    );
  }
}
