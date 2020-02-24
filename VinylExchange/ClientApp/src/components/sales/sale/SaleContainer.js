import React, { Component } from 'react';
import SaleComponent from "./SaleComponent"
import axios from "axios";
import {Url,Controllers,Queries} from "./../../../constants/UrlConstants"

class SaleContainer  extends Component {

    componentDidMount(){
      axios.get(Url.api + Controllers.sales.name + Url.slash + this.props.location.state.saleId)
      .then(response=>{
           console.log(response.data)
      })
      .catch(error=>{

      });
    }


    render() { 
        return (<SaleComponent/>);

    }

}
 
export default SaleContainer;