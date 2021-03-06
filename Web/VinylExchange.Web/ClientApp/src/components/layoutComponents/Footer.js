import React, { Component } from "react";
import {Link} from "react-router-dom";

class Footer extends Component {
  constructor(props) {
    super(props);
    this.state = {};
  }
  render() {
    return (
      <footer>
        <div className="footer" id="footer">
          <div className="container">
              
            <div className="row justify-content-center">
              <div className="col-lg-6 col-md-6 col-sm-6 col-xs-6 text-center align-self-center">
              <img src="img/appLogo.png" heigth="500px" width="350px" />
              </div>
              <div className="col-lg-3 col-sm-2 col-xs-3">
                <h3> </h3>
                <ul>
                 
                  <br />
                 
                </ul>
              </div>
              <div className="col-lg-3 col-sm-2 col-xs-3">
                <ul>
                  <li>
                    <h5>
                      {" "}
                      <Link to="/" style={{marginTop: "3em"}}>Home</Link>
                     
                    </h5>
                    <hr/>
                  </li>
                  <li>
                    {" "}
                    <h5>
                    <Link to="/Marketplace" >Marketplace</Link>
                    </h5>
                                <hr/>
                  </li>
                
                </ul>
              </div>
            </div>
          </div>

          <div className="footer-bottom">
            <div className="container">
              <p className="pull-left copyright">
                {" "}
                Copyright © VinylExchange 2020. All right reserved.{" "}
              </p>
            </div>
            <br/>
           
          </div>
          <br/>
        </div>
      </footer>
    );
  }
}

export default Footer;
