import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, FormsModule, NonNullableFormBuilder, ReactiveFormsModule, Validators } from '@angular/forms';
import { NzAutocompleteModule } from 'ng-zorro-antd/auto-complete';
import { AsyncPipe, CommonModule } from '@angular/common';

import { HttpErrorResponse } from '@angular/common/http';
import { NzFormModule } from 'ng-zorro-antd/form';
import { NzButtonModule } from 'ng-zorro-antd/button';
import { NzIconModule } from 'ng-zorro-antd/icon';
import { Observable } from 'rxjs';
import { SearchService } from '../Services/search.service';
import { Search } from '../Inteface/search';
import { APIResult } from '../Inteface/general';

@Component({
  selector: 'app-search',
  standalone: true,
  imports: [FormsModule, ReactiveFormsModule, AsyncPipe,NzAutocompleteModule,ReactiveFormsModule,NzFormModule,NzButtonModule,NzIconModule],
  templateUrl: './search.component.html',
  styleUrl: './search.component.css'
})
export class SearchComponent implements OnInit {
  searchdata!: any[];;

  filteredOptions!: any[];
  autoTextForm: FormGroup;

  constructor(private searchService: SearchService,
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
          this.AutoSearch();
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
  ngOnInit() {
    this.AutoSearch();
    this.autoTextForm.get('name')?.valueChanges.subscribe(value => this.SearchItem(value));

  }

  AutoSearch() {
    this.searchService.SearchList().subscribe({
      next: (data: APIResult<Search[]>) => {
        this.filteredOptions = data.value.map(item=>item.name) // Assuming the Search interface has a 'name' property
        console.log("search", data);
      },
      error: (error: HttpErrorResponse) => {
        console.error('Error:', error);
      },
    });
  }

  SearchItem(value:string) {
    const query = value.trim();
    if (query.length < 3) {
      this.filteredOptions = [];
      return;
    }
    this.searchService.SearchItems(query).subscribe({
      next: (data: APIResult<Search[]>) => {
        this.filteredOptions = data.value.map(item=>item.name);
        console.log('filter data', this.filteredOptions);
        
      },
      error: (error: HttpErrorResponse) => {
        console.error('Error:', error);
      }
    });
  }

  compareFun = (o1: Search | string, o2: Search): boolean => {
    if (o1) {
      return typeof o1 === 'string' ? o1 === o2.name : o1.id === o2.id;
    } else {
      return false;
    }
  };
}
