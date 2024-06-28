import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, NonNullableFormBuilder, ReactiveFormsModule, Validators } from '@angular/forms';
import { NzAutocompleteModule } from 'ng-zorro-antd/auto-complete';
import { CommonModule } from '@angular/common';
import { SearchService } from '../../Services/search.service';
import { Search } from '../../Inteface/search';
import { APIResult } from '../../Inteface/general';
import { HttpErrorResponse } from '@angular/common/http';
import { NzFormModule } from 'ng-zorro-antd/form';
import { NzButtonModule } from 'ng-zorro-antd/button';
import { NzIconModule } from 'ng-zorro-antd/icon';

@Component({
  selector: 'app-auto-complete',
  standalone: true,
  imports: [NzAutocompleteModule,ReactiveFormsModule,NzFormModule,NzButtonModule,NzIconModule],
  templateUrl: './auto-complete.component.html',
  styleUrl: './auto-complete.component.css'
})
export class AutoCompleteComponent implements OnInit {
  filteredOptions: any[] = [];
  searchdata: any[] = [];
  
  autoTextForm: FormGroup;

  constructor(private searchService:SearchService,
    private fb: NonNullableFormBuilder
  ) {
    this.autoTextForm = this.fb.group({
      name: [],
      id: [0],
      });
   }
  
  submitForm(): void {
    if (this.autoTextForm.valid) {
      this.searchService.addItems(this.autoTextForm.value).subscribe({
        next: (response) => {
          this.autoTextForm.reset();
          this.fetchSearchData();
        },
      })
    } else {
      Object.values(this.autoTextForm.controls).forEach(control => {
        if (control.invalid) {
          control.markAsDirty();
          control.updateValueAndValidity({ onlySelf: true });
        }
      });
    }
    
  }

  ngOnInit(): void {
    this.fetchSearchData();
    this.autoTextForm.get('name')?.valueChanges.subscribe(value => this.onChange(value));
  }

//get data
  fetchSearchData(): void {
    this.searchService.SearchList().subscribe({
       next: (data: APIResult<Search[]>) => {
        this.searchdata = data.value.map(item =>item.name)
        .sort((a, b) => a.localeCompare(b)); // Assuming the Search interface has a 'name' property
      },
      error: (error: HttpErrorResponse) => {
        console.error('Error:', error);
      },
    });
  }

 // here three character after change is hit
  onChange(value: string): void {
    if (value && value.length >= 3 ) {
      this.filteredOptions = this.searchdata.filter(option =>
        option.toLowerCase().includes(value.toLowerCase())
      );
    }
    else{
      this.filteredOptions=[];
    }
  }
}