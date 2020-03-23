import React, { Component } from "react";
import MarketplaceComponent from "./MarketplaceComponent";

class MarketplaceContainer extends Component {
  constructor() {
    super();
    this.state = {
      searchValue: "",
      filterStyleIds: [],
      filterGenreId:""
    };
  }

  onUpdateSearchValue = newSearchValue => {
    if (this.state.searchValue !== newSearchValue) {
      this.setState({ searchValue: newSearchValue });
    }
  };

  onUpdateFilterValue = (styleIds,genreId) => {
    console.log(this.state.filterStyleIds)
    console.log(genreId)
    
    
    if (
      JSON.stringify(this.state.filterStyleIds) !== JSON.stringify(styleIds)
      || this.state.filterGenreId !== genreId
    ) {
      
      this.setState({ filterStyleIds: styleIds,filterGenreId:genreId != undefined ? genreId : "" });
    }
  };

  render() {
    return (
      <MarketplaceComponent
        functions={{
          onUpdateFilterValue: this.onUpdateFilterValue,
          onUpdateSearchValue: this.onUpdateSearchValue
        }}
        data={{
          searchValue: this.state.searchValue,
          filterStyleIds: this.state.filterStyleIds,
          filterGenreId:this.state.filterGenreId
        }}
      />
    );
  }
}

export default MarketplaceContainer;
