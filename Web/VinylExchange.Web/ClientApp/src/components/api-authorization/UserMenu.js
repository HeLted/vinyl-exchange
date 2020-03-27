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

  handleDropDownMenuToggle = (isDropDownActive) => {
    this.setState(prevState => {
      return { isDropDownActive };
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
      <Fragment >
        <li  onMouseLeave={()=>setTimeout(this.handleDropDownMenuToggle(false),200)} className="nav-item dropdown">
          <NavLink
            className="navbar-link btn btn-outline-light text-light"
            style={{whiteSpace:"nowrap"}}
            onMouseOver={()=> this.handleDropDownMenuToggle(true)}
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
            <Link className="dropdown-item" to="/User/Profile">
              Profile
            </Link>
              <Link className="dropdown-item" to="/User/Collection">
              Collection
            </Link>
            
          </div>
        </li>
        <NavItem>
          <NavLink
            tag={Link}
            className="navbar-link btn btn-outline-light text-light"
            to="/Authentication/Logout"
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
            to="/Authentication/Register"
          >
            Register
          </NavLink>
        </NavItem>
        <NavItem>
          <NavLink
            tag={Link}
            className="navbar-link btn btn-outline-light text-light"
            to="/Authentication/Login"
          >
            Login
          </NavLink>
        </NavItem>
      </Fragment>
    );
  }
}

export default UserMenu;
