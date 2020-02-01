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
     
    }
  })

  
  render() {
    return  <MarketplaceComponent  onUpdateSearchValue={this.onUpdateSearchValue} searchValue={this.state.searchValue}/>
     
  }
}
