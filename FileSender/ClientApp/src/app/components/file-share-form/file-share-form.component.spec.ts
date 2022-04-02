import { ComponentFixture, TestBed } from '@angular/core/testing';

import { FileShareFormComponent } from './file-share-form.component';

describe('FileShareFormComponent', () => {
  let component: FileShareFormComponent;
  let fixture: ComponentFixture<FileShareFormComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ FileShareFormComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(FileShareFormComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
