import React, { useState } from "react";
import { Option } from 'react-bootstrap-typeahead/types/types';
import './App.css';
import { Typeahead } from 'react-bootstrap-typeahead';
import { ListGroup, OverlayTrigger, Table, Tooltip } from 'react-bootstrap';
import { Deck, loadDecks } from './models/deck';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import { faCalendar, faCodeFork, faComment, faHeart, faStar } from '@fortawesome/free-solid-svg-icons';

import { Investigator, loadInvestigatorMap } from './models/investigator';
import { createRef, useRef } from 'react';
import { faGithub } from "@fortawesome/free-brands-svg-icons";

function App() {
  const typeaheadRef = createRef<React.ElementRef<typeof Typeahead>>();
  const investigatorMap = loadInvestigatorMap();
  const [decks, setDecks] = useState(Array<Deck>());

  function onFocus() {
    typeaheadRef?.current?.clear();
    setDecks(Array<Deck>());
  }

  async function onChange(selected: Option[]) {
    console.log(selected);
    if (selected.length > 0) {
      typeaheadRef.current?.blur();
      setDecks(Array<Deck>());
    }

    const investigators = investigatorMap.get(selected[0] as string);

    if (!investigators) {
      setDecks(Array<Deck>());
      return;
    }

    const deckPromises = investigators.map((investigator: Investigator) => loadDecks(investigator.id));
    const decksArray = await Promise.all(deckPromises)
    setDecks(decksArray.flat().sort((a, b) => b.favorites - a.favorites));
  }

  function getFormattedDate(date: Date) {
    let year = date.getFullYear();
    let month = (1 + date.getMonth()).toString().padStart(2, '0');
    let day = date.getDate().toString().padStart(2, '0');
  
    return month + '/' + day + '/' + year;
  }

  function onClickDeck(deck: Deck) {
    window.open(`https://arkhamdb.com/decklist/view/${deck.id}`);
  }

  return (
    <div 
      id="main" 
      className="d-flex flex-column unselectable justify-content-center align-items-center"
    >
      <div 
        id="wrapper" 
        className="d-flex flex-column rounded m-2 p-3 highlight"
      >
        <h1 id="title" className="text-center unselectable mb-4">ArkhamDB Search</h1>

        <div id="description" className="text-center rounded p-3 mt-1 mb-3 bg-secondary">
            <h4>Search popular decks for Arkham Horror ivnestigators.</h4>
        </div>

        <Typeahead
          ref={typeaheadRef}
          id="investigator-search"
          labelKey="Name"
          options={[...investigatorMap.keys()]}
          placeholder="Search for an investigator" 
          onChange={onChange}
          onFocus={onFocus}
          className="mb-3"
        />

        <ListGroup>
          {decks.map((deck) => 
            <ListGroup.Item 
              key={deck.id}
              className="d-flex deck justify-content-end" 
              onClick={()=>onClickDeck(deck)}
            >
              <div id="name" className="d-flex flex-grow-1 flex-column text-truncate">
                <strong>{deck.name}</strong>
              </div>
              <div className="d-flex flex-column text-nowrap">
                <div className="d-flex flex-row">
                  <OverlayTrigger 
                    placement="bottom"
                    overlay={
                      <Tooltip>Published</Tooltip>
                    }
                  >
                    <div id="calendar" className="ms-2 text-muted">
                      <FontAwesomeIcon icon={faCalendar} /> {getFormattedDate(deck.published)}
                    </div>
                  </OverlayTrigger>
                  <OverlayTrigger 
                    placement="bottom"
                    overlay={
                      <Tooltip>Likes</Tooltip>
                    }
                  >
                    <div id="likes" className="ms-3 text-danger">
                      <FontAwesomeIcon icon={faHeart} /> {deck.likes}
                    </div>
                  </OverlayTrigger>
                  <OverlayTrigger 
                    placement="bottom"
                    overlay={
                      <Tooltip>Favorites</Tooltip>
                    }
                  >
                    <div id="favorites" className="ms-2 text-warning">
                      <FontAwesomeIcon icon={faStar} /> {deck.favorites}
                    </div>
                  </OverlayTrigger>
                  <OverlayTrigger 
                    placement="bottom"
                    overlay={
                      <Tooltip>Comments</Tooltip>
                    }
                  >
                    <div id="comments" className="ms-2 text-success">
                      <FontAwesomeIcon icon={faComment} /> {deck.comments}
                    </div>
                  </OverlayTrigger>
                  <OverlayTrigger 
                    placement="bottom"
                    overlay={
                      <Tooltip>Version</Tooltip>
                    }
                  >
                    <div id="version" className="ms-2 text-primary" data-bs-title="Version">
                      <FontAwesomeIcon icon={faCodeFork} /> {deck.version}
                    </div>
                  </OverlayTrigger>
                </div>
              </div>
            </ListGroup.Item>
          )}
        </ListGroup>       
      </div>
      <div id="footer" className="d-flex mt-2 mb-2 justify-content-center">
          <span className="text-secondary">
            Source code 
            <a id="github" className="btn btn-sm" href="https://github.com/adstep/arkhamdb-search">
              <FontAwesomeIcon icon={faGithub} size={'2x'}/>
            </a>
          </span>
        </div>
    </div>
  );
}

export default App;
