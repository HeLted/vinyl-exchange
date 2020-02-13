import React, { Component, Fragment } from "react";
import { NavItem, NavLink } from "reactstrap";
import { Link } from "react-router-dom";
import authService from "./AuthorizeService";
import { ApplicationPaths } from "./ApiAuthorizationConstants";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import {
  faUser,
  faAngleDown,
  faAngleUp
} from "@fortawesome/free-solid-svg-icons";
import onClickOutside from "react-onclickoutside";

class UserMenu extends Component {
  constructor(props) {
    super(props);

    this.state = {
      isAuthenticated: false,
      userName: null,
      isDropDownActive: false
    };
  }

  componentDidMount() {
    this._subscription = authService.subscribe(() => this.populateState());
    this.populateState();
  }

  componentWillUnmount() {
    authService.unsubscribe(this._subscription);
  }

  async populateState() {
    const [isAuthenticated, user] = await Promise.all([
      authService.isAuthenticated(),
      authService.getUser()
    ]);
    this.setState({
      isAuthenticated,
      userName: user && user.name
    });
  }

  handleClickOutside = () => {
    if (this.state.isDropDownActive) {
      this.handleDropDownMenuToggle();
    }
  };

  handleDropDownMenuToggle = () => {
    this.setState(prevState => {
      return { isDropDownActive: prevState.isDropDownActive ? false : true };
    });
  };

  render() {
    const { isAuthenticated, userName } = this.state;
    if (!isAuthenticated) {
      const registerPath = `${ApplicationPaths.Register}`;
      const loginPath = `${ApplicationPaths.Login}`;
      return this.anonymousView(registerPath, loginPath);
    } else {
      const profilePath = `${ApplicationPaths.Profile}`;
      const logoutPath = {
        pathname: `${ApplicationPaths.LogOut}`,
        state: { local: true }
      };
      return this.authenticatedView(userName, profilePath, logoutPath);
    }
  }

  authenticatedView(userName, profilePath, logoutPath) {
    return (
      <Fragment>
        <li className="nav-item dropdown">
          <NavLink
            className="navbar-link btn btn-outline-light text-light"
            onClick={this.handleDropDownMenuToggle}
          >
            <FontAwesomeIcon icon={faUser} /> {userName}{" "}
            <FontAwesomeIcon
              icon={this.state.isDropDownActive ? faAngleUp : faAngleDown}
            />
          </NavLink>
          <div
            className="user-dropdown dropdown-menu"
            style={{ display: this.state.isDropDownActive ? "block" : "none" }}
          >
            <a className="dropdown-item" href="#">
              Collection
            </a>
            <Link className="dropdown-item" to={profilePath}>
              Settings
            </Link>
          </div>
        </li>
        <NavItem>
          <NavLink
            tag={Link}
            className="navbar-link btn btn-outline-light text-light"
            to={logoutPath}
          >
            Logout
          </NavLink>
        </NavItem>
      </Fragment>
    );
  }

  anonymousView(registerPath, loginPath) {
    return (
      <Fragment>
        <NavItem>
          <NavLink
            tag={Link}
            className="navbar-link btn btn-outline-light text-light"
            to={registerPath}
          >
            Register
          </NavLink>
        </NavItem>
        <NavItem>
          <NavLink
            tag={Link}
            className="navbar-link btn btn-outline-light text-light"
            to={loginPath}
          >
            Login
          </NavLink>
        </NavItem>
      </Fragment>
    );
  }
}

export default onClickOutside(UserMenu);
