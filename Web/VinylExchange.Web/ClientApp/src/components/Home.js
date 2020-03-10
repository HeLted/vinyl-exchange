import React, { Component } from 'react';
import authService from "./api-authorization/AuthorizeService";

export class Home extends Component {
  static displayName = Home.name;

componentDidMount(){
  authService.getUser().then(resp=>{console.log(resp);})
}

  render () {
    return (
    <div className="home-container align-middle">
      <h1 className="property-text" style={{color:"white"}}>Vinyl Exchange</h1>
      
      <h1 className="property-text" style={{color:"white"}}>Because We Know Vinyl</h1>
    </div>
    );
  }
}
