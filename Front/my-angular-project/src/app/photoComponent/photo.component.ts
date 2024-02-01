import { HttpClient } from '@angular/common/http';
import { Component } from '@angular/core';

@Component({
  selector: 'app-photo',
  standalone: true,
  imports: [],
  templateUrl: './photo.component.html',
  styleUrl: './photo.component.css'
})
export class PhotoComponent {
  
  constructor(private http:HttpClient){

  }
  selectedFile: File | null = null;

  onFileSelected(event: any): void {
    if (event.target && event.target.files && event.target.files.length > 0) {
      this.selectedFile = event.target.files[0];
    } else {
      console.error('No file selected');
    }
  }
  onUpload(){
    if (this.selectedFile === null) {
      console.error('No file selected');
      return;
    }
    const fd = new FormData();
    fd.append('image', this.selectedFile, this.selectedFile.name)
       this.http.post('http://localhost:5164/api/photo/AddPhotoForCity',fd).subscribe(res => {
        console.log(res);
       });
  }
}
