import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ShowRecipientComponent } from './show-recipient.component';

describe('ShowRecipientComponent', () => {
  let component: ShowRecipientComponent;
  let fixture: ComponentFixture<ShowRecipientComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ShowRecipientComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ShowRecipientComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
