import React, {FC,ReactElement,useRef,useEffect,useState} from 'react'
import {CreateNoteDto,Client,NoteLookUpDto}from '../api/api'
import { FormControl } from 'react-bootstrap'

const apiClient=new Client('https://https://localhost:44373');

async function createNote(note:CreateNoteDto) {
    await apiClient.create('1.0',note);
    console.log('Note is created ');
}
const NoteList:FC<{}>=():ReactElement=>{
    let textInput=useRef(null);
    const[notes,setNotes]=useState<NoteLookUpDto[]|undefined>(undefined)
    async function getNotes() {
        const noteListVm=await apiClient.getAll('1.0');
        setNotes(noteListVm.notes);
    }
    useEffect(()=>{
        getNotes();
    },[]);
    const handleKeyPress=(event:React.KeyboardEvent<HTMLInputElement>)=>{
        if(event.key=='Enter'){
            const note:CreateNoteDto={
                title:event.currentTarget.value
            };
            createNote(note);
            event.currentTarget.value='';
            getNotes();
        }
    }
};
