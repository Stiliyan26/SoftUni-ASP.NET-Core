import React, { Fragment, useState, useEffect, useRef } from 'react'

const BOOK_ENDPOINT = 'https://localhost:7251/api/Book';

const Books = () => {
  const [booksTableLine, setBookTableLine] = useState();
  const [newBookInfo, setNewBookInfo] = useState({
    'bookId': 0,
    'bookAuthor': '', 
    'bookTitle': '',
    'bookPublisher': '',
    'borrowInfos': []
  });

  const [editMode, setEditMode] = useState(false);

  const bookAuthorRef = useRef();
  const bookTitleRef = useRef();
  const bookPublisherRef = useRef();
  
  useEffect(() => {
    fetch(BOOK_ENDPOINT)
      .then(res =>  res.json())
      .then(
        dataRow => setBookTableLine(dataRow.map(row => {
          return (
            <tr key={row.bookId}>
              <td>{row.bookAuthor}</td>
              <td>{row.bookTitle}</td>
              <td>{row.bookPublisher}</td>
              <td>
                <button onClick={() => onDeleteRecord(row.bookId)}>Delete</button>
                <button onClick={() => onEditRecord(row)}>Edit</button>
              </td>
            </tr>
          );
        }))
      )
  }, []);

  function onDeleteRecord(recordId) {
    fetch(`${BOOK_ENDPOINT}/${recordId}`, {
      method: 'DELETE'
    })
    .then(function() {
      window.location.reload(true);
    })
    .catch(function(err) {
      console.log(err);
    });
  }

  function onAddRecord(){
    fetch(BOOK_ENDPOINT, {
      method: 'POST',
      headers: {
        'Content-Type': 'application/json'
      },
      body: JSON.stringify({
        "bookId": newBookInfo.bookId,
        "bookAuthor": newBookInfo.bookAuthor,
        "bookTitle": newBookInfo.bookTitle,
        "bookPublisher": newBookInfo.bookPublisher,
        "borrowInfos": []
      })
    })
    .then(function(){
      window.location.reload(true);
    })
    .catch(function(err){
      console.log(err);
    });
  }

  function editRecord(){
    fetch(`${BOOK_ENDPOINT}/${newBookInfo.bookId}`, {
      method: 'PUT',
      headers: {
        'Content-Type': 'application/json'
      },
      data: {
        "id": newBookInfo.bookId
      },
      body: JSON.stringify({
        "bookId": newBookInfo.bookId,
        "bookAuthor": newBookInfo.bookAuthor,
        "bookTitle": newBookInfo.bookTitle,
        "bookPublisher": newBookInfo.bookPublisher,
        "borrowInfos": []
      })
    })
    .then(function(){
      window.location.reload(true);
    })
    .catch(function(err){
      console.log(err);
    });

    setEditMode(mode => !mode);
  }

  function onEditRecord(bookRow){
    bookAuthorRef.current.value = bookRow.bookAuthor;
    bookTitleRef.current.value = bookRow.bookTitle;
    bookPublisherRef.current.value = bookRow.bookPublisher;

    bookAuthorRef.current.focus();  

    setEditMode(mode => !mode);

    setNewBookInfo(info => {
      return {
        ...info,
        "bookId": bookRow.bookId,
        "bookAuthor": bookAuthorRef.current.value,
        "bookTitle": bookTitleRef.current.value,
        "bookPublisher": bookPublisherRef.current.value,
        "borrowInfos": []
      }
    })
  }

  function setValues(event){
    const { name, value } = event.target;

    setNewBookInfo(info => {
      return {
        ...info,
        [name]: value
      }
    });
  }

  return (
      <Fragment>
          <div className='topSection'>
            <i>Books</i>
            <input type="text" id="author" onChange={setValues} 
                name="bookAuthor" value={newBookInfo.bookAuthor}
                ref={bookAuthorRef}
            />
            <input type="text" id="bookTitle" onChange={setValues} 
                name="bookTitle" value={newBookInfo.bookTitle}
                ref={bookTitleRef}
            />
            <input type="text" id="bookPublisher" onChange={setValues} 
                name="bookPublisher" value={newBookInfo.bookPublisher}
                ref={bookPublisherRef}
            />

            <button onClick={onAddRecord}> - Add - </button>
            {editMode === true 
              ? <button onClick={() => {editRecord()}}>- Save Changes -</button>
              : <p></p>
            }

          </div>
          <br></br>
          <table>
              <thead>
                <tr>
                  <th>Author</th>
                  <th>Book Title</th>
                  <th>Book Publisher</th>
                  <th>Actions</th>
                </tr>
              </thead>
              <tbody>
                  {booksTableLine}
              </tbody>
          </table>
      </Fragment>
    )
}

export default Books;