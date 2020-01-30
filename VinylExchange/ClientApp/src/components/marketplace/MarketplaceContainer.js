import React from "react";
import MarketplaceComponent from "./MarketplaceComponent";

export class MarketplaceContainer extends React.Component {
  
  constructor(){

    super()
    this.state = {
      searchValue:""
    }
  }


  onUpdateSearchValue = ((newSearchValue)=>{

    if(this.state.searchValue !== newSearchValue){
      this.setState({searchValue:newSearchValue})
      console.log("updated")
    }

  })

  // shouldComponentUpdate(nextProps, nextState){
  

  //  console.log(nextProps, nextState);
  //  console.log(this.props, this.state);
  //  return   true
  // }
  
  render() {
    return  <MarketplaceComponent  onUpdateSearchValue={this.onUpdateSearchValue} searchValue={this.state.searchValue}/>
     
  }
}
